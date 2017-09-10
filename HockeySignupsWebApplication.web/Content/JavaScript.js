$(function () {
    $(".firstName, .lastName, .email").on('keyup', function () {
        $(".signUp").attr('disabled', !IsValid());
    });
    $(window).load(function () {
        $(".signUp").attr('disabled', !IsValid);
    });
    $(".date, .maxPlayers").on('keyup', function () {
        $(".submit").attr('disabled', !IsValid(true));
    });

    function IsValid(IsAdmin) {
        if (IsAdmin != true) {
            var firstName = $(".firstName").val();
            var lastName = $(".lastName").val();
            var email = $(".email").val();
            return firstName && lastName && email;
        }
        else {
            var date = $(".date").val();
            var maxPlayers = $(".maxPlayers").val();
            return date && maxPlayers != 0;
        }      
    }
});