from datetime import timedelta
from fastapi import APIRouter, Depends, HTTPException, status
from fastapi.responses import JSONResponse
from fastapi.security import OAuth2PasswordRequestForm
from app.error_codes.error_codes import ErrorCode, ErrorMessage
from app.schemas.token import Token, VerifyTokenRequest
from app.schemas.session import SessionResponse
from app.security.auth import create_access_token, pwd_context, ACCESS_TOKEN_EXPIRE_MINUTES, verify_token
from app.db.database import get_db
from sqlalchemy.ext.asyncio import AsyncSession
import asyncio

router = APIRouter(tags=["User"])



