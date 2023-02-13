export default class Utils {
    static transformDateToInt(date) {
        var selectedDate = new Date(date);
        return selectedDate.getFullYear() * 10000 + (selectedDate.getMonth()  + 1) * 100 + selectedDate.getDate();
    }

    static transformIntToDate(date) {
        return new Date(Math.floor(date / 10000), (Math.floor(date / 100) - 1) % 100, date % 100);
    }
}