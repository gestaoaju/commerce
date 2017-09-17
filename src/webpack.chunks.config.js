// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

const path = require('path');
const assets = path.resolve(__dirname, 'Assets');

module.exports = {
    'css/common.min.css': [
        'normalize.css/normalize.css',
        'font-awesome/css/font-awesome.css',
        `${assets}/css/compile.scss`
    ],
    'js/common.min.js': [
        'moment/src/moment.js',
        'numeral/src/numeral.js',
        'vue/dist/vue.js',
        'vue-resource/dist/vue-resource.js',
        'vuelidate/dist/vuelidate.min.js',
        'vuelidate/dist/validators.min.js',
        `${assets}/js/config/moment.config.es6`,
        `${assets}/js/config/numeral.config.es6`,
        `${assets}/js/config/vue.config.es6`
    ],
    'js/overview.min.js': `${assets}/js/app/dashboard/overview.component.es6`,
    'js/signin.min.js': `${assets}/js/app/account/users/signin/signin.component.es6`,
    'js/signup.min.js': `${assets}/js/app/account/users/signup/signup.component.es6`
};
