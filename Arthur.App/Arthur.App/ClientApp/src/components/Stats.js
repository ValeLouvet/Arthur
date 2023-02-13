import React, { Component } from 'react';
import Utils from '../utils';

export class Stats extends Component {
    static displayName = Stats.name;

    constructor(props) {
        super(props);
        this.state = { stats: [], loading: true };
    }

    componentDidMount() {
        this.populateStats();
    }

    static renderStatsTable(statsSummary) {
        return (
            <>
                {statsSummary.reverse().sort((x, y) => x - y).map(stats =>
                    <div key={Utils.transformDateToInt(stats.start)}>
                        <h2>{stats.start.toLocaleDateString()} - {stats.end.toLocaleDateString()}</h2>
                        <h3>Summary Data</h3>
                        <table className='table table-striped' aria-labelledby="tabelLabel">
                            <thead>
                                <tr>
                                    <th>Metric</th>
                                    <th>Prime</th>
                                    <th>Even</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>Average Request time</td>
                                    <td>{stats.averagePrime}</td>
                                    <td>{stats.averageEven}</td>
                                </tr>
                            </tbody>
                        </table>
                        <h3>Daily Uptime Details</h3>
                        <table className='table table-striped' aria-labelledby="tabelLabel">
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Prime</th>
                                    <th>Even</th>
                                </tr>
                            </thead>
                            <tbody>
                                {stats.quarters.map(s =>
                                    <tr key={s.date}>
                                        <td>{s.date}</td>
                                        <td>{s.prime}</td>
                                        <td>{s.even}</td>
                                    </tr>
                                )}
                            </tbody>
                        </table>
                    </div>
                )}
            </>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : Stats.renderStatsTable(this.state.stats);

        return (
            <div>
                <h1>Stats</h1>
                {contents}
            </div>
        );
    }

    async populateStats() {
        const response = await fetch('stats');
        const data = await response.json();
        var dataSummary = this.createDataSummary(data);
        this.setState({ stats: dataSummary, loading: false });
    }

    createDataSummary(data) {
        const dataSummary = [];
        var today = new Date();
        var startDate = new Date(new Date().toDateString());
        startDate.setFullYear(startDate.getFullYear() - 1);
        var lastDate = new Date();
        lastDate.setDate(lastDate.getDate() - 1);
        var lastDayOfQuarterMonth = this.getLastQuarterDay(startDate, lastDate);
        var loopCount = 0;
        while (startDate < today && loopCount < 5) {
            var quarterStats = this.createQuaters(data, startDate, lastDayOfQuarterMonth);
            var primeQuaterlyData = data.filter(x => x.type === 0 && x.date >= Utils.transformDateToInt(startDate.toDateString()) && x.date <= Utils.transformDateToInt(lastDayOfQuarterMonth.toDateString()));
            var evenQuaterlyData = data.filter(x => x.type === 1 && x.date >= Utils.transformDateToInt(startDate.toDateString()) && x.date <= Utils.transformDateToInt(lastDayOfQuarterMonth.toDateString()));
            dataSummary.push({
                start: new Date(startDate),
                end: new Date(lastDayOfQuarterMonth),
                averagePrime: primeQuaterlyData.length === 0 ? "N/A" : Math.floor(primeQuaterlyData.reduce((x, y) => x + y.responseTime, 0) / primeQuaterlyData.length),
                averageEven: evenQuaterlyData.length === 0 ? "N/A" : Math.floor(evenQuaterlyData.reduce((x, y) => x + y.responseTime, 0) / evenQuaterlyData.length),
                quarters: quarterStats
            });

            startDate = new Date(lastDayOfQuarterMonth.getFullYear(), lastDayOfQuarterMonth.getMonth(), lastDayOfQuarterMonth.getDate() + 1);
            lastDayOfQuarterMonth = this.getLastQuarterDay(startDate, lastDate);
            ++loopCount;
        }

        return dataSummary;
    }

    getLastQuarterDay(dateStart, lastDate) {
        var quarterMonth = (Math.floor(dateStart.getMonth() / 3)) * 3 + 3;
        var newLastDayOfQuarterMonth = new Date(dateStart.getFullYear(), quarterMonth, 0);
        return new Date(Math.min(lastDate, newLastDayOfQuarterMonth));
    }

    createQuaters(data, dateStart, dateEnd) {
        var quarterStats = [];
        var currentDate = new Date(dateEnd);
        while (currentDate >= dateStart) {
            var primeData = data.filter(x => x.type === 0 && x.date === Utils.transformDateToInt(currentDate));
            var evenData = data.filter(x => x.type === 1 && x.date === Utils.transformDateToInt(currentDate));
            quarterStats.push({
                date: currentDate.toLocaleDateString(),
                prime: primeData.length === 0 ? "N/A" : (Math.floor(primeData.reduce((x, y) => x + y.responseTime, 0) / primeData.length)).toString(),
                even: evenData.length === 0 ? "N/A" : (Math.floor(evenData.reduce((x, y) => x + y.responseTime, 0) / evenData.length)).toString()
            });
            currentDate.setDate(currentDate.getDate() - 1);
        }

        return quarterStats;
    }
}
