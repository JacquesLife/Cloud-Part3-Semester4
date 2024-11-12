// Video Control Code
// Get the video element
const video = document.getElementById("background-video");

// Set the time (in seconds) when the video should stop
const stopTime = 10; // For example, stop after 10 seconds

// Function to stop the video
function stopVideo() {
    if (video) {
        video.pause(); // Pause the video
    }
}

// Set a timeout to stop the video after the specified time
setTimeout(stopVideo, stopTime * 1000); // Convert seconds to milliseconds

// Claims Processing Code
$(document).ready(function () {
    // Track button functionality
    $('.track-button').click(function () {
        var claimId = $(this).data('claim-id');
        $('.claim-item[data-claim-id="' + claimId + '"] .claim-details').toggle();
    });

    // Approve button functionality
    $('.approve-button').click(function () {
        var claimId = $(this).data('claim-id');
        updateClaimStatus(claimId, 'Approved');
    });

    // Reject button functionality
    $('.reject-button').click(function () {
        var claimId = $(this).data('claim-id');
        updateClaimStatus(claimId, 'Rejected');
    });

    // Function to update the claim status
    function updateClaimStatus(claimId, status) {
        $.ajax({
            url: '/Processing/UpdateStatus',
            type: 'POST',
            data: { claimId: claimId, status: status },
            success: function (result) {
                if (result.success) {
                    var claimItem = $('.claim-item[data-claim-id="' + claimId + '"]');
                    claimItem.find('.status-pending').removeClass('status-pending').addClass('status-' + status.toLowerCase()).text(status);
                    claimItem.find('.approve-button, .reject-button').remove();
                    alert('Claim status updated successfully!');
                } else {
                    alert('Failed to update claim status. Please try again.');
                }
            },
            error: function () {
                alert('An error occurred while updating the claim status. Please try again.');
            }
        });
    }

    // File upload validation
    $('#file-upload').on('change', function () {
        const fileInput = this;
        const fileNameLabel = $('#file-name-label');
        const fileError = $('#file-error');
        const fileName = fileInput.files.length > 0 ? fileInput.files[0].name : 'No file chosen';
        fileNameLabel.text(fileName);

        // Validate file type and size
        const allowedExtensions = ['.pdf', '.docx', '.xlsx', '.txt', '.md'];
        const maxFileSizeBytes = 10 * 1024 * 1024; // 10 MB
        const fileExtension = fileName.split('.').pop().toLowerCase();
        const fileSize = fileInput.files.length > 0 ? fileInput.files[0].size : 0;

        if (!allowedExtensions.includes('.' + fileExtension) || fileSize > maxFileSizeBytes) {
            fileError.show();
        } else {
            fileError.hide();
        }
    });

    // Form submission Validation
    $('#claim-form').submit(function (event) {
        const fileInput = $('#file-upload')[0];
        const fileName = fileInput.files.length > 0 ? fileInput.files[0].name : '';
        const fileError = $('#file-error');

        if (fileName === '' || fileError.is(':visible')) {
            event.preventDefault();
            alert('Please upload a valid file before submitting the form.');
        }
    });

    // Product Management Code
    function loadProducts() {
        $.ajax({
            url: '/Home/GetProducts',
            type: 'GET',
            success: function(products) {
                const tbody = $('#productsList');
                tbody.empty();
                
                products.forEach(function(product) {
                    tbody.append(`
                        <tr>
                            <td>${product.productName}</td>
                            <td>${product.price}</td>
                            <td>${product.category}</td>
                            <td>${product.availability}</td>
                            <td>${product.customerName}</td>
                            <td><span class="status-badge status-${product.orderStatus}">${product.orderStatus}</span></td>
                            <td>${product.country}</td>
                        </tr>
                    `);
                });
            }
        });
    }

    function showAlert(message, isSuccess) {
        const alert = $('#alertMessage');
        alert.removeClass('alert-success alert-error');
        alert.addClass(isSuccess ? 'alert-success' : 'alert-error');
        alert.text(message);
        alert.fadeIn();
        setTimeout(() => alert.fadeOut(), 3000);
    }

    $(document).ready(function() {
        loadProducts();

        $('#productForm').on('submit', function(e) {
            e.preventDefault();
            
            const formData = {
                ProductName: $('#ProductName').val(),
                Price: $('#Price').val(),
                Category: $('#Category').val(),
                Availability: $('#Availability').val(),
                CustomerName: $('#CustomerName').val(),
                OrderStatus: $('#OrderStatus').val(),
                Country: $('#Country').val()
            };

            $.ajax({
                url: '/Product/Work',
                type: 'POST',
                data: formData,
                success: function(response) {
                    showAlert('Product added successfully!', true);
                    $('#productForm')[0].reset();
                    loadProducts();
                },
                error: function(xhr, status, error) {
                    showAlert('Error adding product: ' + error, false);
                }
            });
        });
    });
});