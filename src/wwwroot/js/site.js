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

/// bind onClick event for id is create-OTT button, append a OTT input group after create button,
/// bind remove function to remove button for new Added input group
$("#create-OTT").click(function () {
    $(this).after('<div class="form-group border border-light border-right-0 border-left-0 rounded OTT-group">' +
        '<div class= "form-group"> ' +
        '<label class="white-font" >Platform</label>' +
        '<button class="btn btn-danger custom-btn remove-OTT" type="button">-</button>' +
        '<input class="form-control" type="text" name="OTT_dynamic_platform" placeholder="Platform"/>' +
        ' </div >' +
        ' <div class="form-group">' +
        '<label class="white-font">Url</label>' +
        '<input class="form-control" type="text" name="OTT_dynamic_url" placeholder="Platform URL" />' +
        '</div>' +
        '<div class="form-group">' +
        '<label class="white-font">Icon</label>' +
        '<input class="form-control" type="text" name="OTT_dynamic_icon" placeholder="Platform Icon" />' +
        '</div>' +
        '</div >')
    $(".remove-OTT").click(function () {
        $(this).parent().parent().remove()
    })
})

/// bind onclick event to all OTT remove button
$(".remove-OTT").click(function () {
    $(this).parent().parent().remove()
})

/// Dark mode feature
const toggle = document.getElementById('theme-toggle'); // Get the toggle checkbox
const savedTheme = localStorage.getItem('theme'); // Check and apply the saved theme preference on initial load

if (savedTheme === 'dark') {
    document.body.classList.add('dark-theme'); // Apply dark theme if saved preference is dark
    toggle.checked = true; // Set toggle to checked if dark theme was saved
}

// Listen for changes to the toggle switch
toggle.addEventListener('change', () => {
    // Apply dark theme and save preference when toggle is checked
    if (toggle.checked) {
        document.body.classList.add('dark-theme');
        localStorage.setItem('theme', 'dark');
    }

    // Remove dark theme and save preference as light when toggle is unchecked
    if (!toggle.checked) {
        document.body.classList.remove('dark-theme');
        localStorage.setItem('theme', 'light');
    }
});