// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

/// onClick event for create genre button, will append a new genre input group after create button,
/// bind remove function to remove button for new Added input group
$("#create-genre").click(function () {
    $(this).after('<div class="form-group form-inline">' +
        '<input class="form-control" type="text" name="Product.Genre" placeholder="Genre" />' +
        '<button class="btn btn-danger custom-btn remove-genre" type="button">-</button>' +
        '<span class="text-danger field-validation-valid" data-valmsg-for="Product.Genre" data-valmsg-replace="true"></span>' +
        '</div >')
    $(".remove-genre").click(function () {
        $(this).parent().remove()
    })
})

/// bind onclick event to all genre remove button
$(".remove-genre").click(function () {
    $(this).parent().remove()
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