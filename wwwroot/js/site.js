function navigateToPage(url, isAuthenticated) {
    if (isAuthenticated === 'False') {
        window.location.href = '/Identity/Account/Login'; // Redirect to login page
    } else {
        window.location.href = url;
    }
}

document.addEventListener("DOMContentLoaded", function() {
    const buttons = document.querySelectorAll('.button-container button');
    
    buttons.forEach(button => {
        button.addEventListener('click', function(event) {
            const url = event.target.getAttribute('data-url');
            const isAuthenticated = event.target.getAttribute('data-authenticated') === 'true';
            
            // Check if user is authenticated
            if (!isAuthenticated) {
                event.preventDefault();
                window.location.href = '/Identity/Account/Login'; // Redirect to login page
            } else {
                window.location.href = url;
            }
        });
    });
});
