// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

const NUMBER_FORMAT = '0,0.00';

export const currency = function (value) {
    if (!value) return '';
    return numeral(value).format(`$ ${NUMBER_FORMAT}`);
};

export const number = function (value) {
    if (!value) return '';
    return numeral(value).format(NUMBER_FORMAT);
};
