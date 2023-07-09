# activation_code.py
from sqlalchemy import Column, Float, Integer, String, ForeignKey, JSON
from app.db.database import Base


class Score(Base):
    __tablename__ = "scores"

    id = Column(Integer, primary_key=True, index=True)
    username = Column(String, index=True)
    completion_time = Column(Float)
