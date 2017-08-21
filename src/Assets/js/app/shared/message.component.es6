// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import Vue from 'vue';

export default Vue.component('app-message', {
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
    template: `
        <div class="modal" :class="[mode, { 'show': visible }]">
            <div class="modal-content">
                <slot></slot>
            </div>
        </div>
    `,
    watch: {
        visible() {
            if (this.visible) {
                document.body.classList.add('modal-open')
            } else {
                document.body.classList.remove('modal-open')
            }
        }
    }
});
