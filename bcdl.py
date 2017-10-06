from __future__ import unicode_literals
from lxml import html
from lxml.html import parse
import requests
import youtube_dl
import re
import sys

class MyLogger(object):
    def debug(self, msg):
        pass

    def warning(self, msg):
        pass

    def error(self, msg):
        pass

def my_hook(d):
    if d['status'] == 'finished':
        print('Done downloading, now converting ...')

def bcdl():
	try:
		req = requests.request('GET', 'https://random-album.com/')
		page = html.fromstring(req.text)
		url = str(page.xpath('//a/@href')[0])
	
		r = re.compile('https://([^.]*).*/album/(.*)')
		band = r.search(url).group(1)
		album = r.search(url).group(2)

		print('Catching Artist: {0}, Album: {1}'.format(band, album))

		ydl_opts = {
		    'format': 'mp3',
		    'postprocessors': [{
		        'key': 'FFmpegExtractAudio',
		        'preferredcodec': 'mp3',
		        'preferredquality': '192',
		    }],
		    'logger': MyLogger(),
		    'progress_hooks': [my_hook],
		    'outtmpl': 'downloads/{0} - {1}/%(title)s.%(ext)s'.format(band, album),
		}
	
		with youtube_dl.YoutubeDL(ydl_opts) as ydl:
			print('Downloading ...')
			ydl.download([url])
		print('Done!')
		return

	except KeyboardInterrupt:
	   	print('Interrupted!')

	except:
		print('Error, trying another ...')
		bcdl()	

count = int(sys.argv[1])
for i in range(count):
	bcdl()		  