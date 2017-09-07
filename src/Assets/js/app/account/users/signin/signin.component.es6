// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import Vue from 'vue';
import VueResource from 'vue-resource';
import { ApiResponse } from 'lib/api-response.es6';
import { ApiUser } from 'lib/api-user.es6';
import { SigninViewModel } from './signin.viewmodel.es6';
import { email, required, minLength } from 'vuelidate/lib/validators';
import 'app/shared/message.component.es6';

export default new Vue({
    el: '#signin',
    data: {
        loading: false,
        invalid: false,
        errors: null,
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

            this.$http.post('signin', new SigninViewModel(this.$data))
                .then((response) => {
                    new ApiUser(response.body).save();
                    window.location.href = '/dashboard';
                }).catch((response) => {
                    this.errors = new ApiResponse(response).getErrors();
                    this.loading = false;
                });
        },
        tryAgain() {
            this.invalid = false;
            this.errors = null;
        }
    }
});
