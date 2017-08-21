// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import Vue from 'vue';
import Vuelidate from 'vuelidate';
import VueResource from 'vue-resource';
import moment from 'moment';
import numeral from 'numeral';

Vue.use(Vuelidate);
Vue.use(VueResource);

Vue.filter('currency', function (value) {
    if (!value) return '';
    return numeral(value).format('$ 0,0.00');
});

Vue.filter('date', function (value) {
    if (!value) return '';
    return moment(value).format('DD/MM/YYYY');
});

Vue.filter('datetime', function (value) {
    if (!value) return '';
    return moment(value).format('DD/MM/YYYY [às] HH:mm');
});
