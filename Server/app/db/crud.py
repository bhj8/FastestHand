import datetime
import math
from sqlalchemy.ext.asyncio import AsyncSession
from app.models.activation_code import ActivationCode
from app.models.chat_session import ChatSession
from app.models.user import User
from app.schemas.user import UserCreate
from app.security.password import get_password_hash
from tools.myutils import utils
import asyncio
from sqlalchemy.future import select
import Globals

from app.models.chat_message import ChatMessage
from app.schemas.chat_message import ChatMessageCreate

