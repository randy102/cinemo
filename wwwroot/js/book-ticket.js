$(function () {
  // Tooltip props shared between proceed button tooltip and available tooltip
  const tooltipSharedProps = {
    delay: {
      show: 100,
      hide: 0,
    }
  }

  let infoSeats = $('.info-content.info-seats');

  // Initialize tooltip for proceed button
  $('#proceed-btn-wrapper').tooltip({
    ...tooltipSharedProps,
    title: function () {
      return "Please choose a ticket and a seat first.";
    }
  });

  // Initialize tooltip for available seats
  $('.seat-available').tooltip({
    ...tooltipSharedProps,
    title: function () {
      return $(this).data('seat');
    },
  });

  // Initialize choose ticket popover for available seats
  $('.seat-available').popover({
    html: true,
    placement: 'top',
    content: function () {
      return $('#choose-ticket-popover').html();
    },
  });

  // On choose ticket popover show
  $('#choose-ticket-popover').on('show.bs.popover', function () {

  });

  const proceedBtn = $('#proceed-btn');

  const proceedBtnWrapper = $('#proceed-btn-wrapper');

  // Toggle chosen class on available seat click
  $('.seat-available').click(function (e) {
    $(this).toggleClass('seat-chosen seat-available');
  });

  // On available seat click
  function onAvailableSeatClick() {
    $(this).tooltip('hide').tooltip('disable');

    //const thisSeat = $(this).data('seat');

    //let chosenSeats = infoSeats.text().split(', ');

    //chosenSeats = chosenSeats[0] === ""
    //  ? [thisSeat]
    //  : [...chosenSeats, thisSeat].sort((a, b) => a.localeCompare(b, 'en', { numeric: true }))

    //infoSeats.text(chosenSeats.join(', '));

    $(this).one('click', onChosenSeatClick);

    //proceedBtn.prop('disabled', false)

    //proceedBtnWrapper.tooltip('disable');
  }

  function onChosenSeatClick() {
    $(this).tooltip('enable').tooltip('show');

    const thisSeat = $(this).data('seat');

    let chosenSeats = infoSeats.text().split(', ');

    chosenSeats = chosenSeats.filter(seat => seat !== thisSeat);

    infoSeats.text(chosenSeats.join(', '));

    $(this).one('click', onAvailableSeatClick);

    if (chosenSeats.length == 0) {
      proceedBtn.prop("disabled", true)

      proceedBtnWrapper.tooltip('enable');
    }
  }

  $('.seat-available').one('click', onAvailableSeatClick);
});