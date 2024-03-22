
import { getRandomNumber } from "./NumberUtils.js";


export const transparentize = (value, opacity) => {
    var alpha = opacity === undefined ? 0.5 : 1 - opacity;
    return colorLib(value).alpha(alpha).rgbString();
}



export const getRandomColorWithOpacity = (opacity) =>{
    var red = getRandomNumber(0, 127);
    var green = getRandomNumber(0, 127);
    var blue = getRandomNumber(0, 127);
    return `rgba(${red}, ${green}, ${blue}, ${opacity})`;
}
