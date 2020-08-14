$('#Email').val("");

$("#btnresetpass").click(function ()
{
    if ($("#Email").val() === "")
    {
        $("#Error").show();
        $('.errormessage').html('Please enter mandatory fields.');
        return false;
    }
    $.ajax({
        url: "/UserManagement/IsEmailExits",
        type: "GET",
        data: { Email: $('#Email').val() },
        async: false,
        success: function (response) {
            if (response === false) {
                $('.errormessage').html("'" + $('#Email').val() + "' not exist.");
                $('#Error').show();
                $(window).scrollTop(0);
                $('#Email').val("");
                $('#Email').focus();
                return false;
            }
            else {
                $('#Error').hide();
                $('.errormessage').html("");
                $("#divreset").hide();
                $("#divsubpassword").hide();
                $("#divmainpassword").show();
                $("#divsub").show();
                return true;
            }
        }
    });
});

$("#btnsubmit").click(function () {
    if ($('#Password').val() != $('#ConfirmPassword').val()) {
        $('.errormessage').html('Password and Confirm password do not match.');
        $('#Error').show();
        $(window).scrollTop(0);
        return false;
    } 
    if ($("#Password").val() === "" || $("#ConfirmPassword").val() === "") {
        $("#Error").show();
        $('.errormessage').html('Please enter mandatory fields.');
        return false;
    }
});

