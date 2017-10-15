/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

const DATE_FORMAT = 'DD/MM/YYYY';
const TIME_FORMAT = 'HH:mm';

export const date = function (value) {
    if (!value) return '';
    return moment(value).format(DATE_FORMAT);
};

export const datetime = function (value) {
    if (!value) return '';
    return moment(value).format(`${DATE_FORMAT} [às] ${TIME_FORMAT}`);
};
