$(function () {

  const showtimeId = new URLSearchParams(window.location.search).get('showtimeId');

  let choosingSeat = "";

  let chosenSeats = {};

  // Tooltip props shared between proceed button tooltip and available tooltip
  const tooltipSharedProps = {
    delay: {
      show: 100,
      hide: 0,
    }
  }

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
      placement: 'top',
      content: () => $('#choose-ticket-popover').html(),
      trigger: 'manual',
      sanitize: false,
    })
    // Available handler on mount
    .on('click', onAvailableSeatClick)
    // Bind click functions to ticket row
    .on('shown.bs.popover', function () {
      $('.ticket-row').on('click', onTicketClick);
    })

  function updateSeats() {
    $('#chosen-seats').html(
      Object.keys(chosenSeats)
        .sort()
        .map(seat => `${seat} - ${chosenSeats[seat]}`)
        .join('<br />')
    );
  }

  // On available seat click
  function onAvailableSeatClick() {
    $('.seat-choosing')
      .popover('hide')
      .toggleClass('seat-choosing seat-available');

    $(this)
      .toggleClass('seat-available seat-choosing')
      .tooltip('hide')
      .tooltip('disable')
      .off('click')
      .on('click', onChoosingSeatClick)
      .popover('show')

    choosingSeat = $(this).data('seat');
  }

  // On choosing seat click (click seat again before choosing a ticket)
  function onChoosingSeatClick() {
    $(this)
      .toggleClass('seat-choosing seat-available')
      .popover('hide')
      .tooltip('enable')
      .tooltip('show')
      .off('click')
      .on('click', onAvailableSeatClick)
  }

  // On chosen seat click
  function onChosenSeatClick() {
    const thisSeat = $(this).data('seat');

    $(this)
      .toggleClass('seat-chosen seat-available')
      .off('click')
      .on('click', onAvailableSeatClick);

    // Remove the hidden input that has this seat
    $('#ticket-for-seat-' + thisSeat).remove();

    // Remove this seat from chosenSeats array
    delete chosenSeats[thisSeat];
    updateSeats();

    // Disable proceed button
    $('#proceed-btn').prop("disabled", true)

    // Enable tooltip on proceed button
    $('#proceed-btn-wrapper').tooltip('enable');
  }

  function onTicketClick() {
    const ticketId = $(this).data('ticket-id');
    const ticketName = $(this).data('ticket-name');

    // Update chosen seats in js
    chosenSeats[choosingSeat] = ticketName;
    updateSeats();

    // Enable proceed button and disable it's tooltip
    $('#proceed-btn').prop('disabled', false)
    $('#proceed-btn-wrapper').tooltip('disable');

    // Add hidden input to form
    $('#ticket-hidden-input')
      .clone()
      .attr('id', 'ticket-for-seat-' + choosingSeat)
      .val([showtimeId, choosingSeat, ticketId].join(','))
      .appendTo('#proceed-btn-wrapper');

    $('.seat-choosing')
      .popover('hide')
      .tooltip('enable')
      .toggleClass('seat-choosing seat-chosen')
      .off('click')
      .on('click', onChosenSeatClick);
  }

});