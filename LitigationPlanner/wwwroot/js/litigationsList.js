function UsersListForLitigation(litigationId) {

    $.ajax({
        type: "GET",
        url: "/UsersListForLitigation",
        data: { id: litigationId },
        contentType: 'application/json',
        success: function (response) {
            $('#lawyersList').html(response);
        },
        error: function (response) {
            var error = 400;
        }

    });
}
