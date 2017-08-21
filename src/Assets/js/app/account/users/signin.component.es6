// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import Vue from 'vue';
import VueResource from 'vue-resource';
import HttpHandler from 'lib/http-handler.es6';
import SigninModel from './signin.model.es6';
import { email, required, minLength } from 'vuelidate/lib/validators';
import 'app/shared/message.component.es6';

export default new Vue({
    el: '#signin',
    data: {
        loading: false,
        failed: false,
        invalid: false,
        error: false,
        username: '',
        password: ''
    },
    validations: {
        username: { required, email },
        password: { required, minLength: minLength(8) }
    },
    methods: {
        signin() {
            if (this.$v.$invalid) {
                this.invalid = true;
                return;
            }

            if (this.loading) return;

            this.loading = true;

            this.$http.post('signin', new SigninModel(this.$data))
                .then((response) => {
                    window.location.href = '/dashboard';
                }).catch((response) => {
                    this.failed = response.status === 401;
                    this.error = response.status !== 401;
                    this.loading = false;
                });
        },
        gotIt() {
            this.invalid = false;
        },
        tryAgain() {
            this.failed = false;
            this.error = false;
        }
    }
});
