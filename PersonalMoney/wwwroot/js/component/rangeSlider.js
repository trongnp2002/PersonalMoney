

export const distributeRange = (data,max) => {
    return ` 
                <div class="form-inline position-relative">
                        <label class="position-absolute top-0 left-0 h6">${data.name}: </label>
                    <div class="range-slider ">
                        <input class="range-slider__range" data-distribute-id='${data.id}' type="range" value="${data.budget}" min="0" max="${max}" step="1">
                        <span class="range-slider__value">${data.budget}</span>
                    </div>
                </div>`
}