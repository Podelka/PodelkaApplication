//Изменить название generalFunctionsForProfileUW.js на generalFunctionsForProfileUWSectionP.js и подключить для Product/Section этот скрипт
function review()
{
    if (localStorage.getItem('previewProdFour') == 'false')
    {
        $('#rightBody #workElement').removeClass('fourElement');
        $('#rightBody #workInfo').removeClass('fourElement');
        $('#previewProdFour').css("opacity", ".4");
        $('#previewProdThree').css("opacity", ".8");
    }
    else
    {
        $('#previewProdThree').css("opacity", ".4");
        $('#previewProdFour').css("opacity", ".8");
    }
};

function active_menu_products(WorkroomId)
{
    $('#rightMenu li').removeClass('active');
    $('#products').addClass('active');
    window.history.pushState(
    "ajax", // any object, that can be retrieved
    'Продукты мастерской', // new page title
    '/Workroom/Profile/' + WorkroomId + '/Products' // new url
    );
};

function active_menu_reviews(WorkroomId)
{
    $('#rightMenu li').removeClass('active');
    $('#reviews').addClass('active');
    window.history.pushState(
    "ajax", // any object, that can be retrieved
    'Продукты мастерской', // new page title
    '/Workroom/Profile/' + WorkroomId + '/Reviews' // new url
    );
};

$(document).ready(function ()
{
    // Подписываемся на навигацию браузера по страницам
    window.onpopstate = function (event)
    {
        if (event.state == "ajax")
            window.location.reload();
        else
            window.history.replaceState("ajax", document.title, window.location.href);
        event.preventDefault();
    };
    // Устанавливаем новый заголовок страницы
    document.title = $("#pageTitle").html();
});

function active_menu_workrooms(UserId) {
    $('#rightMenu li').removeClass('active');
    $('#workrooms').addClass('active');
    window.history.pushState(
    "ajax", // any object, that can be retrieved
    "Мастерские пользователя", // new page title
    "/User/Profile/" + UserId + "/Workrooms" // new url
    );
};
function active_menu_adverts(UserId) {
    $('#rightMenu li').removeClass('active');
    $('#adverts').addClass('active');
    window.history.pushState(
    "ajax", // any object, that can be retrieved
    "Заказы пользователя", // new page title
    "/User/Profile/" + UserId + "/Adverts" // new url
    );
};

function active_menu_bookmarks(UserId) {
    $('#rightMenu li').removeClass('active');
    $('#bookmarks').addClass('active');
    window.history.pushState(
    "ajax", // any object, that can be retrieved
    'Заказы пользователя', // new page title
    '/User/Profile/' + UserId + '/Bookmarks' // new url
    );
};