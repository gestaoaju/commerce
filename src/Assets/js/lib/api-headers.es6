// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

import { ApiUser } from './api-user.es6';

export class ApiHeaders {

    constructor() {
        this['Content-type'] = 'application/json';
        this['Authorization'] = 'Bearer ' + ApiUser.getCurrent().token;
    }

}
