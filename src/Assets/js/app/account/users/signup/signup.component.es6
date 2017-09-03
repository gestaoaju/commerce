// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import Vue from 'vue';
import VueResource from 'vue-resource';
import HttpHandler from 'lib/http-handler.es6';
import SignupViewModel from './signup.viewmodel.es6';
import { email, required, minLength, maxLength } from 'vuelidate/lib/validators';
import 'app/shared/message.component.es6';

export default new Vue({
    el: '#signup',
    data: {
        loading: false,
        invalid: false,
        errors: null,
        name: '',
        username: '',
        password: ''
    },
    validations: {
        name: { required },
        username: { required, email },
        password: { required, minLength: minLength(8), maxLength: maxLength(20) }
    },
    methods: {
        signup() {
            if (this.$v.$invalid) {
                this.invalid = true;
                return;
            }

            if (this.loading) return;

            this.loading = true;

            this.$http.post('signup', new SignupViewModel(this.$data))
                .then((response) => {
                    window.location.href = '/dashboard';
                }).catch((response) => {
                    this.errors = new HttpHandler(response).messages();
                    this.loading = false;
                });
        },
        tryAgain() {
            this.invalid = false;
            this.errors = null;
        }
    }
});
