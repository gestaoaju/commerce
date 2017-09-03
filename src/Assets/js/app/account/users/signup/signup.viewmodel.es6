// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

export default class SignupViewModel {
    constructor(view) {
        this.name = view.name;
        this.email = view.username;
        this.password = view.password;
    }
}
