
$(document).ready(function ()
{
    $("#btnRegister").attr("disabled", true);
    $("#Error").hide();
    $("#Name").val(""); 
    $("#Email").val("")
    $("#Password").val("")
    $("#ConfirmPassword").val("") 
    $("#Name").focs(); 

});

$("#btnRegister").click(function ()
{
    if ($('#Password').val() != $('#ConfirmPassword').val())
    {
        $('.errormessage').html('Password and Confirm password do not match.');
        $('#Error').show();
        $(window).scrollTop(0);
        return false;
    }      
    else if ($("#Name").val() === "" || $("#Email").val() === "" || $("#Password").val() === "" || $("#ConfirmPassword").val() === "")
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

function onchangecEmail()
{

    $.ajax({
        url: "/UserManagement/IsEmailExits",
        type: "GET",
        data: { Email: $('#Email').val() },
        async: false,
        success: function (response) {
            if (response === true) {
                $('.errormessage').html("'" + $('#Email').val() + "' already exist.");
                $('#Error').show();
                $(window).scrollTop(0);
                $('#Email').val("");
                $('#Email').focus();
                return false;
            } else {
                return true;
            }
        }
    });
}
function onchangecheckbox()
{

    if ($("#chkagreement").prop("checked") === true) {
        $("#btnRegister").attr("disabled", false);
        $(window).scrollTop(0);
        return false;
    }
    else
    {
        $("#btnRegister").attr("disabled", true);
        $(window).scrollTop(0);
        return false;
    }
   
}