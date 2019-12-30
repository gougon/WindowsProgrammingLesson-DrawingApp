using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawingModel
{
    public class Constant
    {
        public const string SPACE = " ";
        public const string COMMA = ",";
        public const string TAB = "    ";
        public const string CHANGE_LINE = "\n";
        public const string LEFT_SMALL_BRACKET = "(";
        public const string RIGHT_SMALL_BRACKET = ")";
        public const string LEFT_MIDDLE_BRACKET = "[";
        public const string RIGHT_MIDDLE_BRACKET = "]";
        public const string BACK_SLASH = "\\";
        public const string FORM_APPLICATION_NAME = "DrawingForm";
        public const string APP_APPLICATION_NAME = "DrawingApp";
        public const string CONTENT_TYPE = "application/json";
        public const string CLIENT_SECRET_FILE_NAME = "clientSecret.json";
        public const string FILE_NAME = "shapesInformation.json";
        public const string LINE_TEXT = "Line";
        public const string RECTANGLE_TEXT = "Rectangle";
        public const string SIX_SIDE_TEXT = "Hexagon";
        public const string SELECT_LABEL_TEXT = "Selected : ";
        public const string UNDO_ERROR_MESSAGE = "Cannot undo.";
        public const string REDO_ERROR_MESSAGE = "Cannot redo.";
        public const string CREATE_STATE_ERROR_MESSAGE = "Unknown state type.";
        public const string GET_SELECT_SHAPE_ERROR_MESSAGE = "Can't get select shape";
        public const string CHECK_SAVE_MESSAGE = "Are you sure you want to save?";
        public const int TWO = 2;
        public const int THREE = 3;
        public const int FOUR = 4;
        public const int SIX = 6;
        public const int MARK_CIRCLE_RADIUS = 3;
        public const int POINTER_ERROR = 10;
        public const double ROOT_THREE = 1.732f;
    }
}
