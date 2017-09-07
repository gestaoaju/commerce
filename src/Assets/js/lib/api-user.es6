// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

export class ApiUser {
    constructor(attrs) {
        this.nome = attrs.nome;
        this.token = attrs.token;
    }

    save() {
        localStorage.setItem('user', JSON.stringify(this));
    }

    static getCurrent() {
        var attrs = JSON.parse(localStorage.getItem('user'));
        return new ApiUser(attrs);
    }
}
