// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

export class ApiResponse {
    constructor(response) {
        this.response = response;
    }

    getErrors() {
        switch (this.response.status) {
            case 0:
            case -1: return ["Por favor verifique a conexão com a internet e tente novamente."];
            case 401: return ["E-mail e/ou senha estão incorretos."];
            case 403: return ["Acesso negado."];
            case 404: return ["Não foi possível estabelecer uma comunicação com o servidor."];
            case 422: return this.response.body;
            case 500: return ["Ocorreu algum problema. Por favor tente novamente."];
            default: return [];
        }
    }
}
