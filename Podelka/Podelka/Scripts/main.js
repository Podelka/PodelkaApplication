$(window).on('load , resize', function createSize()
{
    doc_w = $(document).width();
    doc_h = $(document).height();
    cont_w = $('.container').width();
    cont_h = $('.container').height();
    $('.container').css(
    {
        'top': (doc_h - cont_h) / 2,
        'left': (doc_w - cont_w) / 2,
        'opacity': 1
    });
});

$(function ()
{
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

    $('#buttonCreate').ready(function ()
    {
        $('#buttonCreate')
            .prop("disabled", true)
            .css("background", "#cecece");
    });

    $('#agreeRules').click(function ()
    {
        if (!$(this).prop('checked'))
        {
            $('#buttonCreate')
                .prop("disabled", true)
                .css("background", "#cecece");
        }
        else
        {
            $('#buttonCreate')
                .prop("disabled", false)
                .css("background", "#fc6060");
        }
    });
});