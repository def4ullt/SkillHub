import re
from collections import Counter
from fastapi import FastAPI
from pydantic import BaseModel
import nltk
from nltk.sentiment.vader import SentimentIntensityAnalyzer

nltk.download('vader_lexicon', quiet=True)

app = FastAPI(title="MLService", version="1.0")
sia = SentimentIntensityAnalyzer()

STOPWORDS = {
    'the','a','an','and','or','but','in','on','at','to','for','of','with',
    'by','from','is','was','are','were','be','been','have','has','had','do',
    'does','did','will','would','could','should','may','might','can','this',
    'that','these','those','it','its','not','no','so','if','as','up','out',
    'about','also','just','very','too','more','most','some','all','any',
    'each','which','who','what','when','where','how','i','you','he','she',
    'we','they','me','him','her','us','them','my','your','his','our','their',
    'than','then','there','here','get','got','use','used','make','made',
}


class AnalyzeRequest(BaseModel):
    comment: str


class AnalyzeResponse(BaseModel):
    sentiment: str
    score: int  # -1, 0, 1
    keywords: list[str]


def extract_keywords(text: str, n: int = 5) -> list[str]:
    words = re.findall(r'\b[a-zA-Z]{3,}\b', text.lower())
    filtered = [w for w in words if w not in STOPWORDS]
    return [word for word, _ in Counter(filtered).most_common(n)]


@app.post("/analyze", response_model=AnalyzeResponse)
def analyze(req: AnalyzeRequest) -> AnalyzeResponse:
    text = req.comment.strip()
    if not text:
        return AnalyzeResponse(sentiment="Neutral", score=0, keywords=[])

    compound = sia.polarity_scores(text)["compound"]
    if compound >= 0.05:
        sentiment, score = "Positive", 1
    elif compound <= -0.05:
        sentiment, score = "Negative", -1
    else:
        sentiment, score = "Neutral", 0

    keywords = extract_keywords(text)
    return AnalyzeResponse(sentiment=sentiment, score=score, keywords=keywords)


@app.get("/health")
def health():
    return {"status": "ok"}
