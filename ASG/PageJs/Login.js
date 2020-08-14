
$(document).ready(function ()
{
    $("#btnRegister").attr("disabled", true);
    $("#Error").hide();

    $("#Email").val("")
    $("#Password").val("")
    $("#Email").focs(); 

});

$("#btnsubmit").click(function ()
{
     
    if ($("#Email").val() === "" || $("#Password").val() === "")
    {
        $("#Error").show();
        $('.errormessage').html('Please enter mandatory fields.');
        return false;
    }
    else
    {
        $("#Error").hide();
        $('.errormessage').html('');
    }
    return true;
});


