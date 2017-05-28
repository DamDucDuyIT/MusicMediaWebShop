init();
function init() {
    var current = 0;
    var video = $('#video');
    var playlist = $('#playlist');
    var tracks = playlist.find('li a');
    var len = tracks.length - 1;
    video[0].volume = .10;
    video[0].play();
    playlist.find('a').click(function (e) {
        e.preventDefault();
        link = $(this);
        current = link.parent().index();
        run(link, video[0]);
    });
    video[0].addEventListener('ended', function (e) {
        current++;
        if (current == len) {
            current = 0;
            link = playlist.find('a')[0];
        } else {
            link = playlist.find('a')[current];
        }
        run($(link), video[0]);
    });
}
function run(link, player) {
    player.src = link.attr('href');
    par = link.parent();
    par.addClass('active').siblings().removeClass('active');
    player.load();
    player.play();
}