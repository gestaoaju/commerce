// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

export class SigninViewModel {
    constructor(view) {
        this.email = view.username;
        this.password = view.password;
    }
}
