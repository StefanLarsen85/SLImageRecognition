const uri = 'https://jolly-sand-0c1895703.azurestaticapps.net/api/analyzeImage';

function analyzeImage() {
    var imageUrl = document.getElementById('imageUrlInput').value;
    var isValidUrl = validateUrl(imageUrl);
    if (isValidUrl == false) {
        document.getElementById('imageDescription').innerHTML = 'Url not valid';
        return;
    }

    let spinner = document.getElementById("spinner-border");
    spinner.style.visibility = 'visible';

    const jsonBodyItem = {
        imageUrl: imageUrl
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(jsonBodyItem)
    })
        .then(response => {
            return response.json()
        })
        .then(data => {
            var imageDiv = document.getElementById('previewImageContainer');
            imageDiv.innerHTML = "";

            var imgTag = document.createElement('img');
            imgTag.src = imageUrl;
            imgTag.classList = 'img-fluid';

            imageDiv.appendChild(imgTag);

            var fullTextResponse = '<h4>Anylyze result</h4>';

            fullTextResponse += '<p><b>Description</b>: ' + data.description.captions[0].text + '.<p/> ';

            if (data.adult.isAdultContent == false) {
                fullTextResponse += '<b>The image does not contain adult content</b><br />';
            }
            else {
                fullTextResponse += 'The image does contain adult content<br />';
            }

            if (data.adult.isRacyContent == false) {
                fullTextResponse += '<b>The image does not contain racy content</b><br />';
            }
            else {
                fullTextResponse += 'The image does contain racy content<br />';
            }

            fullTextResponse += '<h4>Tags</h4>';

            data.tags.forEach(function (arrayTag) {
                fullTextResponse += 'I am ' + Math.round((arrayTag.confidence * 100 + Number.EPSILON) * 100) / 100 + ' % sure of ' + arrayTag.name + '<br />';
            });


            document.getElementById('imageDescription').innerHTML = fullTextResponse;
            spinner.style.visibility = 'hidden';

            console.log(data)
        })
        .catch(err => {
            document.getElementById('imageDescription').innerHTML = "Something went wrong.<br />Did you forget to type in a image url before clicking Analize image?";
            spinner.style.visibility = 'hidden';
        })
}

function validateUrl(str) {
    var pattern = new RegExp('^(https?:\\/\\/)?' + // protocol
        '((([a-z\\d]([a-z\\d-]*[a-z\\d])*)\\.)+[a-z]{2,}|' + // domain name
        '((\\d{1,3}\\.){3}\\d{1,3}))' + // OR ip (v4) address
        '(\\:\\d+)?(\\/[-a-z\\d%_.~+]*)*' + // port and path
        '(\\?[;&a-z\\d%_.~+=-]*)?' + // query string
        '(\\#[-a-z\\d_]*)?$', 'i'); // fragment locator return !!pattern.test(str);
}
