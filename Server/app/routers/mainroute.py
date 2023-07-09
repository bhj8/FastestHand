from datetime import datetime, timedelta
import random
import re
from fastapi import APIRouter, Depends, HTTPException, Request, status
from fastapi.responses import JSONResponse
from pydantic import BaseModel, ValidationError
from sqlalchemy.ext.asyncio import AsyncSession
from fastapi.security import OAuth2PasswordRequestForm

from app.db.database import get_db
from app.models.score import Score
from app.schemas.user import UserCreate
from app.error_codes.error_codes import ErrorCode, ErrorMessage
from tools.api_tx_sms import TencentSmsSender
from tools.mylog import logger
router = APIRouter(tags=["User"])





from sqlalchemy import select


class ScoreIn(BaseModel):
    username: str
    completion_time: float

class Utils:
    regex = ""

    @staticmethod
    def initialize():
        with open('badword.txt', 'r', encoding='UTF-8') as f:
            bad_words = [line.strip() for line in f]
        pattern = r'\b\S*(' + '|'.join(bad_words) + r')\S*\b'
        Utils.regex = re.compile(pattern, re.IGNORECASE)  # Include the flag here


    @staticmethod
    def is_allow_username(username: str):
        if Utils.regex == "":
            Utils.initialize()
        if Utils.regex.search(username):  # No flags needed here
            return False
        return True


@router.post("/updatescores/", response_model=ScoreIn)
async def create_score(score_in: ScoreIn, db: AsyncSession= Depends(get_db)):
    # Check if username contains any bad words
    if not Utils.is_allow_username(score_in.username):
        print("Username contains inappropriate content")
        return JSONResponse(
            content={
                "status": 1002,
                "message": "Username contains inappropriate content",
            },)
    score = Score(**score_in.dict())
    db.add(score)
    await db.commit()
    return score_in


@router.post("/getscores/")
async def get_scores(db: AsyncSession = Depends(get_db)):
    result = await db.execute(select(Score).order_by(Score.completion_time).limit(20))
    scores = result.scalars().all()
    return {"scores": scores}



# api_tx_sms = TencentSmsSender()  # Initialize your SMS sender class
# ip_request_count = {}  # A temporary dictionary to store the IP address and count
# phone_number_verification_codes = {"15079857414":"025025"}  # A temporary dictionary to store phone numbers and their verification codes
# MAX_REQUESTS_PER_IP = 5  # Set the maximum requests per IP, you can change this value

# class PhoneNumberRequest(BaseModel):
#     phone_number:  str


# @router.post("/send_verification_code", response_class=JSONResponse)
# async def send_verification_code(phone_number_request: PhoneNumberRequest, request: Request, db: AsyncSession = Depends(get_db)):
#     global ip_request_count
#     global phone_number_verification_codes

#     # Get the client's IP address
#     client_ip = request.headers.get("X-Real-IP") or request.headers.get("X-Forwarded-For") or request.client.host

#     # Remove expired requests
#     now = datetime.now()
#     if client_ip in ip_request_count:
#         ip_request_count[client_ip] = [t for t in ip_request_count[client_ip] if now - t < timedelta(hours=24)]

#     # Check if the IP has reached the request limit
#     if client_ip in ip_request_count and len(ip_request_count[client_ip]) >= MAX_REQUESTS_PER_IP:
#         return JSONResponse(content={"status": "Error", "message": "你的网络已达到今日最大短信限制，请明天再试"})

#     # Update the IP request count
#     if client_ip not in ip_request_count:
#         ip_request_count[client_ip] = []
#     ip_request_count[client_ip].append(now)

#     # Generate a random 6-digit verification code
#     verification_code = random.randint(100000, 999999)
#     verification_code =str(verification_code)


#     try:
#         # Call the send_sms method from your SMS sender class
#         api_tx_sms.send_sms(phone_number_request.phone_number, [str(verification_code)])
        
#         #成功了再改验证码信息
#         # Store the phone number and verification code in the temporary dictionary
#         phone_number_verification_codes[phone_number_request.phone_number] = verification_code
#         return JSONResponse(content={"status": "Success", "message": "Verification code sent"})
#     except Exception as e:
#         logger.error(f"Error sending SMS: {e}")
#         return JSONResponse(content={"status": "Error", "message": str(e)}, status_code=500)
    
    
    



