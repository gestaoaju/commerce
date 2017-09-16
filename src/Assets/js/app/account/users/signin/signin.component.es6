// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import Vue from 'vue';
import VueResource from 'vue-resource';
import { email, required, minLength } from 'vuelidate/lib/validators';
import { AppComponent } from 'app/app-component.es6';
import { ApiResponse } from 'lib/api/api-response.es6';
import { SigninViewModel } from './signin.viewmodel.es6';

export default new AppComponent({
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
            this.invalid = this.$v.$invalid;

            if (!(this.invalid || this.loading)) {
                this.loading = true;

                this.$http.post('signin', new SigninViewModel(this.$data))
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
