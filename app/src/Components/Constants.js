export const baseUrl = 'https://localhost:7124';

//ref: https://stackoverflow.com/a/55556258/725957
export const currencyFormat = (num) => {
    if (num == '' || num == null || num == undefined) {
        return '$0.00';
    }

    if (isNaN(num)) {
        return num;
    }

    num = +num;
    return '$' + num.toFixed(2).replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
};

export const currencyToNumber = (currency) => {
    if (currency == null || currency == undefined) {
        return 0;
    }
    
    if (currency[0] != '$') {
        return currency;
    }

    const num = currency.replace(/[,$]/g, '');
    return +num;
};

export const dateFormat = (date) => {
    if (date == undefined) {
        return '';
    }

    if (date?.length < 10) {
        return date;
    }

    return new Date(date).toLocaleDateString();
}

export const toDate = (dateString) => {
    const date = new Date(dateString);
    return date.toISOString();
}