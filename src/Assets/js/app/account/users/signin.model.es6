// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

export default class SigninModel {
    constructor(attrs) {
        this.email = attrs.username;
        this.password = attrs.password;
    }
}
