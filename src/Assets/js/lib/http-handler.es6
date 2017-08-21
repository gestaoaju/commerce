// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

export default class HttpHandler {
    constructor(response) {
        this.response = response;
    }

    message() {
        switch (this.response.status) {
            case 0:
            case -1: return "Por favor verifique a conexão com a internet e tente novamente.";
            case 401: return "Usuário e/ou senha inválidos.";
            case 403: return "Acesso negado.";
            case 404: return "Não foi possível estabelecer uma comunicação com o servidor.";
            case 500: return "Ocorreu um problema. Por favor tente novamente.";
            default: return this.response.body;
        }
    }
}
