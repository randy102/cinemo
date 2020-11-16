(function () {
  'use strict';
  window.addEventListener(
    'load',
    function () {
      var forms = document.getElementsByClassName('needs-validation');

      var validation = Array.prototype.filter.call(forms, function (form) {
        form.addEventListener(
          'submit',
          function (event) {
            if (form.checkValidity() === false) {
              event.preventDefault();
              event.stopPropagation();
            }

            form.classList.add('was-validated');
          },
          false
        );
      });
    },
    false
  );
})();

function checkPw(form) {
  pw1 = form.pw1.value;
  pw2 = form.pw2.value;

  if (pw1 != pw2) {
    alert('\nPasswords does not match!');
    return false;
  } else return true;
}

function checkFn(form) {
  fn1 = form.fn1.value;
  fn2 = form.fn2.value;

  if (fn1 != fn2) {
    alert('\nNames does not match!');
    return false;
  } else return true;
}
