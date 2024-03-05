export const statusMessage = (isSuccess, message) => {
    const div = $('<div></div>');
    const button = $('<button></button>');
    const span = $('<span></span>');
    //div
    const classType = isSuccess ? 'alert-success ': 'alert-danger '
    div.addClass(`alert ${classType} alert-dismissible`);
    div.attr('role', 'alert');
    //button
    button.attr('type', 'button');
    button.attr('data-dismiss', 'alert');
    button.attr('aria-label', 'Close');
    button.addClass('close');
    //span
    span.attr('aria-hidden', 'true');
    span.append('&times;')
    button.append(span);
    div.append(button);
    div.append(message);
    return div;

}