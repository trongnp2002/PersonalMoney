
function convertNumber(input) {
    var number = input.toString().replace(/[^0-9]/g, ""); // Remove non-digit characters
    var convertedText = convertToVietnameseText(number);
    convertedText = convertToVietnameseText(number);
    document.getElementById("convertedText").textContent = convertedText;
    // document.getElementById("editConvertedText").textContent = convertedText;
}


function convertNumberEdit(input, id) {
    var number = input.toString().replace(/[^0-9]/g, ""); // Remove non-digit characters
    var convertedText = convertToVietnameseText(number);
    document.getElementById("editConvertedText" + id).textContent = convertedText;
}

function convertToVietnameseText(number) {
    const ones = ['', ' một ', ' hai ', ' ba ', ' bốn ', ' năm ', ' sáu ', ' bảy ', ' tám ', ' chín '];
    const tens = ['', 'mười', 'hai mươi', 'ba mươi', 'bốn mươi', 'năm mươi', 'sáu mươi', 'bảy mươi', 'tám mươi', 'chín mươi'];
    const scales = ['', ' nghìn', ' triệu', ' tỷ'];

    let numArray = number.toString().split('').reverse();
    let result = [];

    if (numArray.length > 12) {
        // throw new Error('Number too large');
        return document.getElementById("convertedText").textContent = "Number too large!";

    }

    for (let i = 0; i < numArray.length; i += 3) {
        let chunk = parseInt(numArray.slice(i, i + 3).reverse().join(''));

        if (chunk !== 0 || i === 0) {
            let scale = scales[Math.floor(i / 3)];
            let text = ' ';

            if (chunk >= 100) {
                text += ones[Math.floor(chunk / 100)] + ' trăm ';
                chunk %= 100;
            }

            if (chunk >= 10 && chunk <= 19) {
                text += ' mười ';
                chunk %= 10;
            } else if (chunk >= 20 || chunk === 0) {
                text += tens[Math.floor(chunk / 10)] + ' ';
                chunk %= 10;
            }

            if (chunk !== 0) {
                if (text.endsWith('mươi ')) {
                    text = text.slice(0, -6);
                }
                if (chunk === 1 && i === 0 && scale === '') {
                    text += ' một ';
                } else if (chunk === 4 && i === 0 && scale === '') {
                    text += ' tư ';
                } else if (chunk === 5 && i === 0 && scale === '') {
                    text += ' năm ';
                } else {
                    text += ones[chunk];
                }
            }

            if (scale !== ' ') {
                text += scale;
            }

            result.push(text);
        }
    }

    return result.reverse().join(' ');
}





