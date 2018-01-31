$(function () {
    $('#Entrar').on('click', function () {
        loginView.login();
    });

    $('#EsqueciMinhaSenha').on('click', function () {
        loginView.obterEsqueciMinhaSenha();
    });

    $('#' + system.constants.defaultModalId).on('click', '#EnviarEsqueciMinhaSenha', function () {
        loginView.esqueciMinhaSenha();
    });
});

var loginView = function () {

    var login = function () {

        var data = { Login: $('#Login').val(), Senha: $('#Senha').val() };

        system.postJsonAjax("Conta/Login", data).done(function (data) {

            if (data.Success) {

                if (data.Other.AlterarSenha)
                    window.location = data.Other.UrlAlterarSenha;
                else
                    window.location = data.Content;
            } else {
                if (data.Content)
                    system.showDangerAlert(data.Content, "messageContainer");

                $('#divDetalhe').html(data.View);
            }
        });
    }

    var esqueciMinhaSenha = function () {

        var data = { Email: $('#Email').val() };

        system.postJsonAjax("Conta/EsqueciMinhaSenha", data).done(function (data) {

            if (data.Success) {
                $('#' + system.constants.defaultModalId).modal('hide');
                system.showInfoAlert(data.Content, "messageContainer");
            } else {
                system.loadHtmlInMainModal(data.View);

                if (data.Content)
                    system.showDangerAlert(data.Content, "modalMessageContainer");
            }
        });
    }

    var obterEsqueciMinhaSenha = function () {
        system.loadInMainModal('Conta/EsqueciMinhaSenha');
    }

    return {
        login: login,
        esqueciMinhaSenha: esqueciMinhaSenha,
        obterEsqueciMinhaSenha: obterEsqueciMinhaSenha
    };
}();