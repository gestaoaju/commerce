/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

import $ from 'jquery';
import Vue from 'vue';

export const Message = Vue.extend({
    template: `
        <div class="message" :class="[mode, { 'show': visible }]">
            <div class="message-content">
                <slot></slot>
            </div>
        </div>
    `,
    props: {
        visible: Boolean,
        mode: {
            type: String,
            default: 'success',
            validator: function (value) {
                return /success|info|danger/.test(value);
            }
        }
    },
    watch: {
        visible() {
            if (this.visible) {
                $('body').addClass('message-show');
            } else {
                $('body').removeClass('message-show');
            }
        }
    }
});
