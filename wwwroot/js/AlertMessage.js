function confirmDelete(callback) {

    Swal.fire({
        title: 'Are you sure?',
        text: "This record will be deleted!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#d33',
        cancelButtonColor: '#6c757d',
        confirmButtonText: 'Yes, delete it!',
        cancelButtonText: 'Cancel'
    }).then((result) => {

        if (result.isConfirmed) {
            callback();
        }

    });

}

function showWarning() {

    Swal.fire({
        icon: 'warning',
        title: 'Warning',
        text: 'Please select at least one data to delete.',
        confirmButtonColor: '#f0ad4e'
    });

}
