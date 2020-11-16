function confirmDelete({ form, title, message }) {
  bootbox.confirm({
    title: title,
    centerVertical: true,
    message: message,
    buttons: {
      confirm: {
        label: '<i class="fas fa-trash mr-1"></i> Yes',
        className: 'btn-danger',
      },
      cancel: {
        label: 'Cancel',
      },
    },
    callback: function (result) {
      result && form.submit();
    },
  });
}
