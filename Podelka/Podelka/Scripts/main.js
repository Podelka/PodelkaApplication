$(function ()
{
    $(window).load(function ()
    {
        var hh = $('#header').outerHeight(true);
        var fh = $('#footer').outerHeight(true);
        var wh = $(window).outerHeight(true);
        var �h = wh - hh - fh;
        $('#mainContent').css('min-height', �h);
    });

    $(window).resize();
    //��������� � generalFunctionsForProfileUW.js
    $('#workElement').ready(function ()
    {
        if (localStorage.getItem('previewProdFour') == 'false')
        {
            Preview3();
        }
        else
        {
            $('#previewProdThree').css("opacity", ".4");
            $('#previewProdFour').css("opacity", ".8");
        }
    });

    $('#buttonCreate').ready(function ()
    {
        if (!$('#agreeRules').prop('checked'))
        {
            $('#buttonCreate')
                //.prop("disabled", true)
                .css("background", "#cecece");
        }
    });

    $('#agreeRules').click(function ()
    {
        if (!$(this).prop('checked'))
        {
            $('#buttonCreate')
                //.prop("disabled", true)
                .css("background", "#cecece");
        }
        else
        {
            $('#buttonCreate')
                //.prop("disabled", false)
                .css("background", "#fc6060");
        }
    });

    $('#findButton').click(function (e)
    {
        e.preventDefault();
        $(this).css(
        {
            position: "absolute",
            right: "0px"
        });
        $(".findLine").css("display", "block");
    });

    $('#findButton').dblclick(function (e)
    {
        e.preventDefault();
        $(".findLine").css("display", "none");
    });
    //��������� � generalFunctionsForProfileUW.js
    $('#previewProdThree').click(function ()
    {
        Preview3();
    });
    //��������� � generalFunctionsForProfileUW.js
    $('#previewProdFour').click(function ()
    {
        Preview4();
    });

});

$(window).resize(function createSize()
{
    $('.container').css(
    {
        position: 'absolute',
        left: ($(window).width() - $('.container').outerWidth(true)) / 2,
        top: ($(window).height() - $('.container').outerHeight(true)) / 2
    });
});
//��������� � generalFunctionsForProfileUW.js
function Preview3()
{
    $('#rightBody #workElement').removeClass('fourElement');
    $('#rightBody #workInfo').removeClass('fourElement');
    $('#previewProdFour').css("opacity", ".4");
    $('#previewProdThree').css("opacity", ".8");
    localStorage.setItem('previewProdFour', 'false');
};
//��������� � generalFunctionsForProfileUW.js
function Preview4()
{
    $('#rightBody #workElement').addClass('fourElement');
    $('#rightBody #workInfo').addClass('fourElement');
    $('#previewProdThree').css("opacity", ".4");
    $('#previewProdFour').css("opacity", ".8");
    localStorage.setItem('previewProdFour', 'true');
};