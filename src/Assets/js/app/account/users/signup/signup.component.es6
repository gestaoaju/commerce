// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import Vue from 'vue';
import VueResource from 'vue-resource';
import { ApiResponse } from 'lib/api-response.es6';
import { ApiUser } from 'lib/api-user.es6';
import { SignupViewModel } from './signup.viewmodel.es6';
import { email, required, minLength, maxLength } from 'vuelidate/lib/validators';
import 'app/shared/app-message.component.es6';

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
            this.invalid = this.$v.$invalid;
            
            if (!(this.invalid || this.loading)) {
                this.loading = true;

                this.$http.post('signup', new SignupViewModel(this.$data))
                    .then((response) => {
                        window.location.href = '/dashboard';
                    }).catch((response) => {
                        this.errors = new ApiResponse(response).getErrors();
                        this.loading = false;
                    });
            }
        },
        tryAgain() {
            this.invalid = false;
            this.errors = null;
        }
    }
});
