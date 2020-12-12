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

  $('.seat-available')
    // Initialize tooltip for available seats
    .tooltip({
      ...tooltipSharedProps,
      title: function () {
        return $(this).data('seat');
      },
    })
    // Initialize popover for available seats
    .popover({
      html: true,
      placement: function (popover, seat) {
        return 'top';
      },
      content: () => $('#choose-ticket-popover').html(),
      trigger: 'manual',
      sanitize: false,
    })
    // Available handler on mount
    .on('click', onAvailableSeatClick);


  // On available seat click
  function onAvailableSeatClick() {
    $(this)
      .toggleClass('seat-available seat-choosing')
      .tooltip('hide')
      .tooltip('disable')
      .off('click', onAvailableSeatClick)
      .on('click', onChoosingSeatClick)
      .popover('show')

    // Popover automatically opens

    //const thisSeat = $(this).data('seat');

    //let chosenSeats = infoSeats.text().split(', ');

    //chosenSeats = chosenSeats[0] === ""
    //  ? [thisSeat]
    //  : [...chosenSeats, thisSeat].sort((a, b) => a.localeCompare(b, 'en', { numeric: true }))

    //infoSeats.text(chosenSeats.join(', '));

    //proceedBtn.prop('disabled', false)

    //proceedBtnWrapper.tooltip('disable');


  }

  function onChoosingSeatClick() {
    $(this)
      .toggleClass('seat-choosing seat-available')
      .tooltip('enable')
      .off('click', onChoosingSeatClick)
      .on('click', onAvailableSeatClick)
      .popover('hide')
      .tooltip('show')

    // Popover automatically closes


  }

  // On chosen seat click
  function onChosenSeatClick() {
    $(this)
      .toggleClass('seat-chosen seat-available')
      .off('click', onChosenSeatClick)
      .on('click', onAvailableSeatClick);

    // Remove this seat from chosenSeats array
    let chosenSeats = infoSeats
      .text()
      .split(', ')
      .filter(seat => seat !== $(this).data('seat'))
      .join(', ');

    // Update seats info
    infoSeats.text(chosenSeats);

    // If there are no seats left
    if (chosenSeats.length == 0) {
      // Disable proceed button
      $('#proceed-btn').prop("disabled", true)

      // Enable tooltip on proceed button
      $('#proceed-btn-wrapper').tooltip('enable');
    }
  }



});