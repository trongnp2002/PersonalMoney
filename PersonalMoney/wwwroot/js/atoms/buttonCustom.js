export const createButton = (classes, id='') => {
    const button = $('<button></button>')
    if (Array.isArray(classes) && classes.length > 0) {
        button.addClass(classes.join(' '));
    }
    if(id){
        button.attr('id', id);
    }
    return button;
}

export const buttonTogglePopup = (classes,id='',dataTarget)=>{
    const button = createButton(classes,id);
    button.attr('data-toggle','modal');
    button.attr('data-target',dataTarget);

    return button;
}