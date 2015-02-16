$(function ()
{
    $('#findButton').click(function (e)
    {
        e.preventDefault();
        $('#findButton').css(
        {
            position:"absolute",
            right:"0px"
        });	
        $(".findLine").css("display","block");
    });
    $('#findButton').dblclick(function (e)
    {
        e.preventDefault();
        $(".findLine").css("display", "none");
    });

    $('#buttonCreate').ready(function ()
    {
        $('#buttonCreate').attr("disabled", true);
        $('#buttonCreate').css(
        {
            background: "#cecece"
        });
    });
    $('#agreeRules').click(function ()
    {
        if (!$(this).prop('checked'))
        {
            $('#buttonCreate').attr("disabled", true);
            $('#buttonCreate').css(
            {
                background: "#cecece"
            });
        }
        else
        {
            $('#buttonCreate').attr("disabled", false);
            $('#buttonCreate').css(
            {
                background: "#fc6060"
            });
        }
    });
});

$(window).on('load , resize', function createSize()
{
    doc_w = $(document).width();
    doc_h = $(document).height();
    cont_w = $('.container').width();
    cont_h = $('.container').height();
    $('.container').css({'top':(doc_h-cont_h)/2});
    $('.container').css({'left':(doc_w-cont_w)/2});
    $('.container').css({'opacity':1});
});