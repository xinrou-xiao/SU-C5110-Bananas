// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

/// onClick event for create genre button, will append a new genre input group after create button,
/// bind remove function to remove button for new Added input group
$("#create-genre").click(function () {
    $(this).after('<div class="form-group form-inline">' +
        '<input class="form-control" type="text" name="genre_dynamic" placeholder="Genre" />' +
        '<button class="btn btn-danger custom-btn remove-genre" type="button">-</button>' +
        '<span asp-asp-validation-for="Product.Genre" class="text-danger"></span>' +
        '</div >')
    $(".remove-genre").click(function () {
        $(this).parent().remove()
    })
})

/// bind onclick event to all genre remove button
$(".remove-genre").click(function () {
    $(this).parent().remove()
})