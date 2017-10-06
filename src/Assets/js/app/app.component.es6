// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import Vue from 'vue';
import moment from 'moment';
import numeral from 'numeral';
import { currency, number } from 'lib/formatters/numeric.formatters.es6';
import { date, datetime } from 'lib/formatters/datetime.formatters.es6';
import { Message } from 'app/shared/message.component.es6';

export class AppComponent extends Vue {
    constructor(options) {
        AppComponent.addComponents();
        AppComponent.addFilters();
        AppComponent.bind(options)

        super(options);
    }

    static addComponents() {
        Vue.component('app-message', Message);
    }

    static addFilters() {
        Vue.filter('date', date);
        Vue.filter('datetime', datetime);
        Vue.filter('currency', currency);
        Vue.filter('number', number);
    }

    static bind(options) {
        options.el = '#app';
        options.data = options.data || {};
        options.data.sidebar = { show: false };
        options.data.spinner = { show: false };
        options.methods = options.methods || {};
        options.methods.toggleSidebar = () => {
            options.data.sidebar.show = !options.data.sidebar.show;
        };
    }
}
