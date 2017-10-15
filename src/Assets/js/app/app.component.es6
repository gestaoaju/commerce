/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

import $ from 'jquery';
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
        AppComponent.bindDefaultOptions(options);
        AppComponent.bindSidebarOptions(options);
        AppComponent.bindSpinnerOptions(options);
        AppComponent.registerEvents();

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

    static bindDefaultOptions(options) {
        options.el = '#app';
        options.data = options.data || {};
        options.methods = options.methods || {};
    }

    static bindSidebarOptions(options) {
        options.data.sidebar = {
            show: false
        };

        options.methods.toggleSidebar = () => {
            options.data.sidebar.show = !options.data.sidebar.show;
        };
    }

    static bindSpinnerOptions(options) {
        options.data.spinner = {
            show: false,
            message: null
        };

        options.methods.showSpinner = (message) => {
            options.data.spinner.message = message;
            options.data.spinner.show = true;
        };

        options.methods.hideSpinner = () => {
            options.data.spinner.message = null;
            options.data.spinner.show = false;
        };
    }

    static registerEvents() {
        $(function() {
            const $content = $('.app-content');

            $content.on('scroll', function () {
                if ($content.scrollTop()) {
                    $content.addClass('scrolled');
                } else {
                    $content.removeClass('scrolled');
                }
            });
        });
    }
}
