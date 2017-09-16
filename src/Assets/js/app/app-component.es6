// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import Vue from 'vue';
import moment from 'moment';
import numeral from 'numeral';
import { Message } from 'app/shared/app-message.component.es6';
import { Sidebar } from 'app/shared/app-sidebar.component.es6';

export class AppComponent extends Vue {
    constructor(options) {
        AppComponent.addCustomFilters();
        AppComponent.addSharedComponents();
        AppComponent.bind(options)

        super(options);
    }

    static addCustomFilters() {
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
    }

    static addSharedComponents() {
        Vue.component('app-message', Message);
        Vue.component('app-sidebar', Sidebar);
    }

    static bind(options) {
        options.el = '#app';
    }
}
